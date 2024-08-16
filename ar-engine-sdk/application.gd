extends Control
class_name Application


var event_listeners:= {}
var input_listeners:= []



func _init() -> void:
	G.application_main = self


func api(cmd:String, data=null):
	match cmd:
		'connect_to_hud_app':
			print('[System] Connect to HUD App from SWP App')
			WS.configure(data)
			for i in ['on_message', 'on_error', 'on_connected', 'on_disconnected']:
				if !event_listeners.has(i):
					event_listeners[i] = []
				event_listeners[i].append({
					'target_node' : data['target_node'],
					'method' : data[i]
				})
		'connect_to_input':
			input_listeners.append({
				'target_node' : data['target_node'],
				'method' : data['on_input']
			})
		'event':
			var event_id:= ''
			
			match data:
				Utils.EVENTS.HUDAppConnected:
					event_id = 'on_connected'
				Utils.EVENTS.HUDAppDisconnected:
					event_id = 'on_disconnected'
			
			if event_listeners.has(event_id):
				for i in event_listeners[event_id]:
					if is_instance_valid(i['target_node']) and i['target_node'].has_method(i['method']):
						i['target_node'].call(i['method'])
					else:
						print('[System] Invalid node ', i['target_node'], ' or has no method ', i['method'])
		'message':
			var event_id:= 'on_message'
			if event_listeners.has(event_id):
				for i in event_listeners[event_id]:
					i['target_node'].call(i['method'], data)
		'send_to_hud_app':
			WS.send_data('swp_to_hud', to_json(data))
		'input':
			for i in input_listeners:
				i['target_node'].call(i['method'], data['type'], data['state'])
		_:
			print('api command: ', cmd, ', data: ', to_json(data))


