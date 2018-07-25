#!/usr/bin/env ruby
# -*- coding: utf-8 -*-

require 'pathname'

require 'uri'
require 'net/https'
require 'date'
require 'json'

dev_id = '28-0414681d12ff'
path = Pathname.new('/sys/bus/w1/devices').join(dev_id).join('w1_slave')

buf = path.read

temperature = buf.scan(/t=([0-9]+)/).first.map{|s| s.to_f}.first
temperature = temperature / 1000
puts(temperature)

uri = URI.parse("https://chipaquariummobile.azure-mobile.net/tables/WaterTemperatures")
app_key = 'YhmVlElOaAQsMUoGPijjXYgGipJAiX54'

data = {}
data['aquariumId'] = 2
data['measurementAt'] = DateTime.now.strftime("%Y-%m-%dT%H:%M:%S.%L%Z")
data['temperature'] = temperature

http = Net::HTTP.new(uri.host, uri.port)

http.use_ssl = true
http.verify_mode = OpenSSL::SSL::VERIFY_NONE

http.start do |h|

    request = Net::HTTP::Post.new(uri.path)

    request.set_content_type("application/json")
    request["Accept"] = "application/json"
    request["X-ZUMO-APPLICATION"] = app_key
    
    request.body = JSON.generate(data)
    
    response = http.request(request)
end
