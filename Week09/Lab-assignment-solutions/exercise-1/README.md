# Solution for Lab exercise 1 in week 9 

	virtualenv .
	source bin/activate
	pip install -r requirements.txt
	
You must have a running Memcached server using default host and port (127.0.0.1:11211)

To install Memcached on OSX

	brew install memcached

To install Memcached on Ubuntu

	sudo apt-get install memcached
	
To run memcached in verbose mode

	memcached -vv


Curls for incrementing left and right.


	curl -X post http://localhost:5000/balance/right
	curl -X post http://localhost:5000/balance/left
	
Curls for fetching the values for left and right

	curl -X get http://localhost:5000/balance/right
	curl -X get http://localhost:5000/balance/left
	
