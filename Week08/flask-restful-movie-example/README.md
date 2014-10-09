# Simple Movie API written in Flask-Restful

This code is an example that will be used in the second lecture in the course T-514-VEFT, fall 2014.

This example is the same as the [flask-movie-example](https://github.com/reykjavik-university/2014-T-514-VEFT/tree/master/Week08/flask-movie-example) but in this example we use Flask-restful to write the service.

This example is a simple API written in Flask. This Api supports two HTTP methods on a resource named /movies

## GET /movies
Returns list of movies that have been posted to the api. Format of the response is as follows

	[
	    {
	        "name": "The hackers",
	        "year": 1995
	    },
	    {
	        "name": "The Matix",
	        "year": 1999
	    }
	]
	
You can use cURL to craft a get request to the API as follows

	curl -X GET http://localhost:5000/movies
	
## POST /movies
Allows you to add a new movie to the /movies resource. To add a new movie add a json object in the body of your post request with the following data

	{
		"name": "movie name",
		"year": 1999
	}
	
You can use curl to craft a post request as follows

	curl -H "Content-Type: application/json" -d '{"name":"Terminator 2","year":1997}' http://localhost:5000/movies
	
## Install and run
To install this code, create a virtualenv in the directory as follows

	virtualenv .venv
	
Then activate the virtualenv

	source .venv/bin/activate
	
Install the requirements into your virtualenv with pip

	pip install -r requirements.txt

The requirements are Flask and Flask-restful

To run the API debug server

	python app.py