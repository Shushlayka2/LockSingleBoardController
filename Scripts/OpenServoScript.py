import OPi.GPIO as GPIO
import time

GPIO.setboard(GPIO.ZEROPLUS)
GPIO.setmode(GPIO.BOARD)
GPIO.setup(7, GPIO.OUT)
p = GPIO.PWM(7, 50)
p.start(7.5)
p.ChangeDutyCycle(2.5)
time.sleep(1)
p.stop()
GPIO.cleanup()