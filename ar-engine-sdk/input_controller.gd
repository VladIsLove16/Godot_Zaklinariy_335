extends Node

const SWIPE_THRESHOLD = 20
const DPAD_THRESHOLD = 0.03

var pressed_input:Dictionary = {}

func _ready() -> void:
	pass


func _input(event: InputEvent) -> void:
	if event is InputEventKey:
		if pressed_input.has(event.scancode) and pressed_input[event.scancode] == event.pressed: return
		
		pressed_input[event.scancode] = event.pressed
		match event.scancode:
			KEY_W:
				command('swp_left_joypad_up', event.pressed)
			KEY_UP:
				command('swp_right_joypad_up', event.pressed)
			KEY_S:
				command('swp_left_joypad_down', event.pressed)
			KEY_DOWN:
				command('swp_right_joypad_down', event.pressed)
			KEY_A:
				command('swp_left_joypad_left', event.pressed)
			KEY_LEFT:
				command('swp_right_joypad_left', event.pressed)
			KEY_D:
				command('swp_left_joypad_right', event.pressed)
			KEY_RIGHT:
				command('swp_right_joypad_right', event.pressed)
			KEY_SPACE:
				command('swp_left_joypad_center', event.pressed)
			KEY_ENTER:
				command('swp_right_joypad_center', event.pressed)
	
	elif event is InputEventScreenDrag:
		command('screen_touch_position', {
			'x' : event.position.x,
			'y' : event.position.y,
		})
		
		if event.relative.y < -SWIPE_THRESHOLD:
			command('screen_swipe', 'up')
		elif event.relative.y > SWIPE_THRESHOLD:
			command('screen_swipe', 'down')
		if event.relative.x < -SWIPE_THRESHOLD:
			command('screen_swipe', 'left')
		elif event.relative.x > SWIPE_THRESHOLD:
			command('screen_swipe', 'right')
	
	elif event is InputEventScreenTouch:
		command('screen_touch', event.pressed)

#	elif event is InputEventJoypadMotion:
#		if event.axis == 1:
#			if event.axis_value < -DPAD_THRESHOLD:
#				command('joy_stick', 'up')
#			elif event.axis_value > DPAD_THRESHOLD:
#				command('joy_stick', 'down')
#		elif event.axis == 0:
#			if event.axis_value > DPAD_THRESHOLD:
#				command('joy_stick', 'left')
#			elif event.axis_value < -DPAD_THRESHOLD:
#				command('joy_stick', 'right')
#
#	elif event is InputEventJoypadButton:
#		if event.pressed:
#			match event.button_index:
#				JOY_DPAD_UP:
#					command('joy_button', 'up')
#				JOY_DPAD_DOWN:
#					command('joy_button', 'down')
#				JOY_DPAD_LEFT:
#					command('joy_button', 'left')
#				JOY_DPAD_RIGHT:
#					command('joy_button', 'right')
#

func command(type, state):
	G.application_main.api('input', {
		'type' : type,
		'state' : state
	})
