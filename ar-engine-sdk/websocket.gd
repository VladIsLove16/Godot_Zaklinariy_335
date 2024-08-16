extends Node
class_name WebSocket

const PORT = 9042
const WSU = 'ws://localhost:{port}'

var ws:= WebSocketClient.new()

var ws_url:= WSU.format({
	'port' : PORT
})


func configure(data):
	ws.connect("connection_closed", self, "_on_connection_closed")
	ws.connect("connection_error", self, "_on_error")
	ws.connect("connection_established", self, "_on_connected")
	ws.connect("data_received", self, "_on_data_received")
	try_to_connect()
	G.application_main.api('event', Utils.EVENTS.WaitConnection)


func try_to_connect():
	var err = ws.connect_to_url(ws_url)
	if err != OK:
		set_process(false)
		return
	G.application_main.api('event', Utils.EVENTS.WSCreated)


func _on_connection_closed(was_clean = false):
	set_process(false)
	G.application_main.api('event', Utils.EVENTS.ConnectionClosed)


func _on_error():
	print('[System] Builder is unavailable...')
	set_process(false)
#	get_tree().create_timer(0.5).connect('timeout', self, 'try_to_connect')


func _on_connected(proto = ""):
	print("[System] Connected to Builder!")
	send_data('register', 'swp')
	set_process(true)
	G.application_main.api('event', Utils.EVENTS.WSConnected)


func _on_data_received():
	var pkt = ws.get_peer(1).get_packet()
	var data = JSON.parse(pkt.get_string_from_utf8()).result
	print(data)
	match data['message_type']:
		'ping':
			G.application_main.api('event', Utils.EVENTS.BuilderConnected)
		'server_state':
			if !data['data']:
				G.application_main.api('event', Utils.EVENTS.BuilderDisconnected)
		'apps_connected':
			print("[System] App connected.")
			G.application_main.api('event', Utils.EVENTS.HUDAppConnected)
		'apps_disconnected':
			print("[System] App disconnected.")
			G.application_main.api('event', Utils.EVENTS.HUDAppDisconnected)
		'message':
			G.application_main.api('message', parse_json(data['data']))



func send_data(type: String, data = null):
	if !is_processing(): return
	var packet_data:= {}
	packet_data["message_type"] = type
	packet_data["data"] = data
	ws.get_peer(1).put_packet(JSON.print(packet_data).to_utf8())


func _process(delta):
	ws.poll()
