# Python lab assignment
This document contains lab assignments for the Python part in the course
T-514-VEFT. This is not the final version of this document where new
assignments will be added gradually while we are working with Python and tools
related to Python.

## Exercise 1
Write a Python script that receives a number from std-in and verifies if the
number is odd or even. If the number is even this script prints out "The number
is even", otherwise it prints out "The number is odd"

You can access scripts arguments through a standard library module called
`sys`. In that module you can access a variable (of type list) called `argv`.
For example, you can print out all the arguments passed into a script as
follows.

	import sys

	for x in sys.argv:
		print x

Note that the first value in sys.argv is the script name. You can use list
slicing and the internal function `len()` verify the correctness of the
scripts' input.

You script should work as follows

	% python oddeven.py
	Missing number argument
	% python oddeven.py 2
	The number is even
	% python oddeven.py 3
	The number is odd

## Exercise 2
The first problem from [Project Euler](https://projecteuler.net/) is as follows

> If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
> 
> Find the sum of all the multiples of 3 or 5 below 1000.

Let's alter this problem little bit. Write a Python function named `euler1(n)`. This function should return the sum of all multiples of 3 and 5 below n, greater then 0.

For this I suggest that you look at the internal function `range()`. Range can generate a list of number sequence that you can loop over.

	for x in range(1, 10):
		print x
		
This would be equivalent to Java for loop

	for(int i=1; i<=9; i++) {
		System.out.println(i);
	}

Your code should be similar to the following code skeleton.

	def euler1(n):
		# TODO: Implement
		return 0

	case1 = euler1(10)

	if case1 != 23:
	    print 'script is incorrect for the base case!'
	else:
	    print 'The base case is correct, but is your script correct?'




# Exercise 3
Enough of math problems! Let's start doing something more realistic. Let's write a little Phonebook framework to store contact informations and let's use the OOP features of Python to do so.

Let's start by creating some [DTO](http://en.wikipedia.org/wiki/Data_transfer_object) which represents a Contact entry.

	class Contact(object):
	    def __init__(self, name, address, phone_number):
	        self.name = name
	        self.address = address
	        self.phone_number = phone_number
	
	    def __str__(self):
	        return '{0} {1} {2}'.format(self.name, self.address, self.phone_number)

Place this class into file called contact.py

Open up a interactive Python shell in the same folder as the contact.py file
resides in and create instance of the Contact class to verify that it works.

	Python 2.7.5 (default, Mar  9 2014, 22:15:05)
	[GCC 4.2.1 Compatible Apple LLVM 5.0 (clang-500.0.68)] on darwin
	Type "help", "copyright", "credits" or "license" for more information.
	>>> from contact import Contact
	>>> c1 = Contact('hlysig', 'Huldudalur 9', '8931337')
	>>> print c1
	hlysig Huldudalur 9 8931337
	>>>
	
What does the `__init__` function do and what does the `__str__` function do? Go to the [Python manual](https://docs.python.org/2/reference/datamodel.html) and read about these functions.

Now we have a simple class that can store contact informations.
	
# Exercise 4
Let's write another class that represents a Phonebook. This class should allow
us to add contacts, search for contacts and remove contacts. To begin with,
let's store the contact instances in memory.

	class PhoneBook(object):
		def __init__(self):
			"""
			Empty constructor. The constructor
			creates one member variable, contacts.
			This variable is a dictionary and can be
			used to store contacts based on name.
			"""
			self.contacts = {}
		
		def add(self, contact):
			"""
			Adds contact to the Phonebook.
			Add it to the dictionary using the name as
			the key, and store the contact object in
			the dictionary
			"""
			pass
		
		def search(self, value):
			"""
			Searches for a given contact by a value.
			Should return a List of contacts
			"""
			pass
			
		def all(self):
			"""
			Returns all the contacts, in a List.
			"""
			pass
		
		def remove(self, contact):
			"""
			Remove a contact from the Phonebook
			"""
			pass
		
		def count(self):
			"""
			Returns the number of contacts in the
			Phonebook
			"""
			pass

Implement the missing functions in the class. Place this class after the
Contact class in contact.py

# Exercise 5
After we have implemented the Phonebook we have no idea if it works correctly
or not. We could write a Python code that uses our Phonebook and verify by hand
if it works or not, but let's be more pro than that. Let's write some unit
tests for our implementation.

Python has a built in [Unit test
framework](https://docs.python.org/2/library/unittest.html) that is easy to
use. Skim through it and then let's get busy writing some unit tests.

In file called test_phonebook.py write the following code

	import unittest
	from contact import PhoneBook, Contact
	
	
	class TestPhonebook(unittest.TestCase):
	    def test_add(self):
	        c = Contact('hlysig', 'Husasmidjan 2', '5672323')
	        p = PhoneBook()
	        p.add(c)
	        self.assertTrue(p.count() == 1, msg='After we add one contact, we'
	                        'should have at least one contact in Phonebook')
	
	    def test_search(self):
	        c = Contact('hlysig', 'Husasmidjan 2', '5672323')
	        p = PhoneBook()
	        p.add(c)
	        contacts = p.search('hlysig')
	        self.assertTrue(len(contacts) == 1)
	        self.assertEqual(contacts[0].name, 'hlysig')

Running the test using the Python unit test framework is rather painful, but
there is a fantastic project which extends the Python test framework that makes
this task less easier. This project is called
[nose](https://nose.readthedocs.org/en/latest/).

Before we use nose, we must install it. If you've gone through the Python
environment installation guide that is on Github you have most likely installed
tools called virtualenv and Pip. If not, got to that tutorial and read about
these tools.

Let's create a virtualenv and install nose so we can run the test. In the folder where test_phonebook.py and contacts.py reside, do the following.

	virtualenv .
	source bin/activate
	pip install nose
	deactivate
	source bin/activate

Now you have a working virtualenv enabled with nose installed.

You can now run the tests with a command named `nosetests`

	% nosetests
	..
	----------------------------------------------------------------------
	Ran 2 tests in 0.004s
	
	OK

Write more tests and verify that your implementation is correct.


# Exercise 6
In this exercise we write a simple blog entry page using
[Flask](http://flask.pocoo.org/). Flask is a micro- web framework for Python
based on [Werkzeug](http://werkzeug.pocoo.org/) and [Jinja
2](http://jinja.pocoo.org/docs/dev/).

Before you start we recommend that you look through the quick start guide on
Flask at
[http://flask.pocoo.org/docs/0.10/quickstart/#quickstart](http://flask.pocoo.org/docs/0.10/quickstart/#quickstart).
It is a comprehensive guide which covers most of the features flask provides.

We focus on Flask and intentionally leave out any database layers. That is the
subject for the next exercise. Until then we store our blog entries in memory.

With that said, let us get our hands dirty.

Create a new folder somewhere on your disk. In this example I will store the
application under ~/Desktop/BlogApplication

	mkdir ~/Desktop/BlogApplication; cd $_

In that folder we create a virtualenv for our application, we then enable it
and install Flask.

	virtualenv .
	source bin/activate
	pip install flask

Next create a file called app.py in the folder and write the following code.

	from flask import Flask
	
	app = Flask(__name__)
	
	
	@app.route('/')
	def list_entries():
		return 'viewing index'


	@app.route('/entry/<slug>')
	def view_entry(slug):
		return 'viewing post by slug {0}'.format(slug)
	
	if __name__ == '__main__':
		app.run(debug=True)

Now before we continue let's dissect this code and understand what it is doing.

In the line:

	from flask import Flask
	
we import the Flask module so we can use it. From that module we import a class
named Flask that we use to create a Flask application.

That is exactly what we do next in:

	app = Flask(__name__)

we create instance of the Flask class and we pass in the name of our package,
namely `__name__`. Flask uses this name when it searches for templates and
static files. When you write the whole application in a single python file it
is safe to use `__name__`. You can read about the parameters that you can add
to `Flask` in
[http://flask.pocoo.org/docs/0.10/api/](http://flask.pocoo.org/docs/0.10/api/).

Next we create two functions, `list_entries` and `view_entry`. This is just two
ordinary Python functions. What makes them special are the decorators that are
added on them, namely `app.route`. This decorator allows us to bind Python
functions to a url. When we get a request with the path `/` flask executes the
`list_entries` function. When we get a request with path `/entry/slug` Flask
executes the `view_entry` function. This is how we define routes in Flask.

We use `list_entries` to list all the entries that we have and we use
`view_entry` to view a individual entry by the entry
[slug](http://en.wikipedia.org/wiki/Semantic_URL#Slug) value. 

Then we add main runner,

	if __name__ == '__main__':
		app.run(debug=True)		
	
This allows you to start a debug server while you are developing. Let's do that
and verify that our code works. To start the developer web server, simply
execute the app.py script.

	python app.py
	 * Running on http://127.0.0.1:5000/
	 * Restarting with reloader

If you see this message you have a running developer web server on port 5000.
If you alter any Python code, this server will restart itself so you can try
out our changes directly without manually starting and stopping this server.

Try navigating to

- [http://localhost:5000/](http://localhost:5000/)
- [http://localhost:5000/entry/some-slug](http://localhost:5000/entry/some-slug)
- [http://localhost:5000/entry/another-slug](http://localhost:5000/entry/another-slug)

You should see the text that we return from our functions in app.py. Next we
create a model which represents entries in our system.

Create a new python package named models.py under the same folder as app.py and
add the following code in it.

	from slugify import slugify
	
	
	class Entry(object):
	    def __init__(self, title, blog_text):
	        self.title = title
	        self.blog_text = blog_text
	        # We create slug from the title
	        self.slug = slugify(title)
	
	
	# We create array of entries that we store in memory.
	# We will refactor this out for database later
	entries = []
	
	# We create two dummy posts that we can view on the page
	entries.append(Entry('Some blog title', 'This is my blog text'))
	entries.append(Entry('Some other title', 'This is my second blog text'))


In models.py we use a package called pyhton-slugify to calculate the slug value
for a given entry using the title. You need to install that package into your
virtualenv, as follows.

	pip install python-slugify

As you can see the Entry class is a simple POCO class. We will changes this
into real database model object in the next exercise. We also have a list
variable, named entries, that we use to store entries. They will live in memory
and will be lost when you restart the server.
	
Next we alter our function `list_entries` to render a template that we then
bind to the entries that are stored in `models.entries`. Alter your app.py as
follows.


	from flask import Flask, render_template
	from models import entries
	
	app = Flask(__name__)
	
	
	@app.route('/')
	def list_entries():
	    return render_template('list-entries.html', entries=entries)
	
	
	@app.route('/entry/<slug>')
	def view_entry(slug):
	    return 'viewing post by slug {0}'.format(slug)
	
	if __name__ == '__main__':
	    app.run(debug=True)
	    
as you can see, `list_entries` now renders a template named `list-entries.html`
and binds the entries which are stored in `models.entries`. Let's define this
template.

Flask search for templates in `__name__/templates/`. Thus, you need to create
folder called templates in the same folder as you store your app.py. In that
folder create a new file named list-entries.html and fill it with the following
markup.


	<!DOCTYPE html>
	<html lang="en">
	<head>
	  <meta charset="UTF-8">
	  <title></title>
	</head>
	<body>
	  {% for entry in entries %}
	  <div class="entry">
	    <h1><a href="/entry/{{entry.slug}}">{{entry.title}}</a></h1>
	  </div>
	  {% endfor %}
	</body>
	</html>

Here we are using the Jinja2 template language to loop through the entries that
we bind with the template. We loop through the entries and we create a link to
the entry slug under `/entries/slug` to view the blog post. This is a url to
our second function `view_entry` in app.py

Try it out and check if it works. You should see a list of two entires that you
can click-on.

Now lets implement the `view_entry` function for viewing the blog entry.

We need to create a new template for viewing a given entry, so let´start there. 

Create a new template in `__name__/templates` named view-entry.html and fill it
with something similar as follows.

	<html>
	<head>
	    <title>Viewing entry {{entry.title}}</title>
	</head>
	<body>
	    <h1>{{entry.title}}</h1>
	    <p>{{entry.blog_text}}</p>
	</body>
	</html>

Next we implement `view_entry` as follows.

	from flask import Flask, render_template
	from models import entries
	
	app = Flask(__name__)
	
	
	@app.route('/')
	def list_entries():
	    return render_template('list-entries.html', entries=entries)
	
	
	@app.route('/entry/<slug>')
	def view_entry(slug):
	    found_entries = [x for x in entries if x.slug == slug]
	    if not found_entries:
	        return 'No entry found', 404
	    else:
	        return render_template('view-entry.html', 
	            entry=found_entries[0])
	    return 'viewing post by slug {0}'.format(slug)
	
	if __name__ == '__main__':
	    app.run(debug=True)
	    
Now if you click the entries on the page you should see the blog text for the
entry.

If you want to add some css or js then you can create a folder next to app.py
called static. The url to the static artifacts will then be `/static/foo.css`.

Add some css and play around with the code. Experiment, experiment, experiment.
