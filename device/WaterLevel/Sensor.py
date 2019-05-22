# coding: utf-8

import logging
import RPi.GPIO as GPIO

class Sensor:

    def __init__(self, gpio):
        self._logger = logging.getLogger('Sensor')
        self._gpio = gpio
        GPIO.setmode(GPIO.BCM)
        GPIO.setup(self._gpio, GPIO.IN, pull_up_down=GPIO.PUD_UP)

    def read(self):
        return GPIO.input(self._gpio) == 0