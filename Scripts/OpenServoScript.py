import OPi.GPIO as GPIO

GPIO.setboard(GPIO.ZEROPLUS)
GPIO.setmode(GPIO.BOARD)
GPIO.setup(7, GPIO.OUT)
p = GPIO.PWM(7, 50)
p.start(7.5)
p.ChangeDutyCycle(2.5)
GPIO.cleanup()