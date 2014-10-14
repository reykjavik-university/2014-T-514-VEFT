from flask import Flask, render_template, request, redirect
from db import Session
from models import Movie

app = Flask(__name__)


@app.route('/')
def list_movies():
    year = request.args.get('year')
    session = Session()
    if not year:
        movies = session.query(Movie).all()
    else:
        movies = session.query(Movie).filter(Movie.year == year).all()
    return render_template('list-movies.html', movies=movies)


@app.route('/add', methods=['POST', 'GET'])
def add():
    if request.method == 'GET':
        return render_template('add-movie.html')
    else:
        title = request.form.get('title')
        year = request.form.get('year')
        session = Session()
        m = Movie(title=title, year=year)
        session.add(m)
        session.commit()
        return redirect('/')
