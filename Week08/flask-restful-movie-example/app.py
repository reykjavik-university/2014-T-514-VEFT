import json
from flask import Flask, request
from flask.ext import restful
from movies import movies_db, Movie

app = Flask(__name__)
api = restful.Api(app)


class MovieListResrouce(restful.Resource):
    def get(self):
        return [x.to_dict() for x in movies_db]

    def post(self):
        data = json.loads(request.data)
        movie = Movie(data.get('name'), data.get('year'))
        movies_db.append(movie)
        return 'ok', 200

api.add_resource(MovieListResrouce, '/movies')

if __name__ == '__main__':
    app.run(debug=True)
