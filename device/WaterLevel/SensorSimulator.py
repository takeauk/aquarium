# coding: utf-8
import random

class SensorSimulator:

    def __init__(self):
        pass

    def read(self):
        return bool(random.uniform(0, 1))
