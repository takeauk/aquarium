# coding: utf-8

import logging
import time
import os.path
import re

class Sensor:

    def __init__(self, device):
        self._logger = logging.getLogger('Sensor')
        self._device = device

    def read_temperature(self):
        if not os.path.isfile(self._device):
            raise ValueError(
                'Not exists device {0}.' .format(self._device))
        with open(self._device) as f:
            match = re.search(r't=(?P<value>[0-9]+)', f.read())
        if not match:
            raise ValueError(
                'Does not matched regex expression.')
        return float(match.group('value')) / 1000
