extends AROverlap

var sprite: ARImage
var label: ARLabel

#Time control:
var frame_time: float = 0
var global_timer: float = 0
var fps: float = 16
var global_add: int = 10
var add: int
var total: int = 1

var max_Overlap_h = 840
var max_Overlap_w = 1820 
var min_Overlap_h = 128
var min_Overlap_w=128

func start() -> void:
		sprite = get_ar_element("ARImage")
		label = get_ar_element("../Total/ARLabel")
		api("connect_to_swp_app", {
			"target_node" : self,
			"on_message" : "on_message",
			"on_error" : "on_error",
			"on_connected" : "on_connected",
			"on_disconnected" : "on_disconnected"
		})

func update_process(delta: float) -> void:
	if frame_time >= 1.0 / fps:
		global_timer += frame_time
#		timer_print(global_timer)
		api('send_to_swp_app', str(global_timer))
		api('out' , str(global_timer))
		size_up(global_add)
		sprite_up(add)
		frame_time = 0
	frame_time += delta
	
func timer_print(time:float)->void:
	label.text = str(time)
	
func size_up (add: int) -> void:
		self.width += add
		self.height += add
		sprite.width += add
		sprite.height += add
		if self.height <= 100: 
			global_add = add*-1
			
func sprite_up (add: int) -> void:
		sprite.frame_coord.x += 1
		
func overlap_start(ar_element: AROverlap)->void:
	if ar_element.name == "Dwarf":
#	ar_element.change_health(health_up)
		label.text = str(total)
		self.top = rand_range(0,  max_Overlap_h-500)
		self.left = rand_range(0, max_Overlap_w-400)
		self.width = min_Overlap_w
		self.height = min_Overlap_h
		sprite.width = min_Overlap_w
		sprite.height = min_Overlap_h		
		total +=1	
	#Границы экрана
	if ar_element.name == "S" or ar_element.name == "N" or   ar_element.name == "W" or  ar_element.name == "E" :
		global_add = global_add*-1
	

