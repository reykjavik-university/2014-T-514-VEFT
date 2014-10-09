# Import json package from python standard library. This module
# can be used to transform json string to more suitable objects
# to work with and to transform lists and dictionaries to json strings
import json

# We import flask and flask artifacts that we use in code.
from flask import Flask, Response, request, render_template

# Module that we use to store movies.
from movies import movies_db, Movie

# We carete instance of flask. Flask is a wrapper around Werkzeug
app = Flask(__name__)


# We create a route for '/'. This route will render the html page
# with help instructions on how to use the movies APIs
@app.route('/')
def index():
    return render_template('index.html')


# We define a route for fetching and posting movies. This route supports
# both get and post methods.
@app.route('/movies', methods=['GET', 'POST'])
def movies():
    # We can ask the current request what method it is. If it is a get
    # return json respond with list of all movies.
    if request.method == 'GET':
        # We create a list of dictionaries for all movies. Here we are
        # using the list comprehensions feature in Python
        return Response(json.dumps([x.to_dict() for x in movies_db]),
                        mimetype='application/json')
    # If it is not a get method, it must be a post method (where we only allow
    # post and get). We use json to change the request data, on json format, to
    # dictionary where we can fetch the details of the movie being posted. We
    # create a new Movie instance and add it to the movie store.
    else:
        data = json.loads(request.data)
        movie = Movie(data.get('name'), data.get('year'))
        movies_db.append(movie)
        return 'Ok', 200

if __name__ == '__main__':
    # We run debug server if we execute this module directly. This is only
    # used when we are developing with Flask. This is not how you would
    # execute this app in production.
    app.run(debug=True)
