# coding: utf-8

import sys
import os
import time
import RPi.GPIO as GPIO


if len(sys.argv) < 1 or (not sys.argv[1].isdigit()):
    print ( "You need to provide the gpio pin number as command line arguments." )
    sys.exit(0)

GPIO_PIN_NUMBER = int(sys.argv[1])
GPIO.setmode(GPIO.BCM)
GPIO.setup(GPIO_PIN_NUMBER, GPIO.IN, pull_up_down=GPIO.PUD_UP)

while True:
    GPIO.wait_for_edge(GPIO_PIN_NUMBER, GPIO.FALLING)
    while True:
        if GPIO.input(GPIO_PIN_NUMBER) == 0:
            print ("Shutdown now.")
            os.system("sudo shutdown -h now")
            break
        else:
            break
        time.sleep(0.1)