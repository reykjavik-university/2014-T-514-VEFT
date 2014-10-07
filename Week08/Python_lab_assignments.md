# Python lab assignment
This document contains lab assignments for the Python part in the course T-514-VEFT. This is not the final version of this document where new assignments will be added gradually while we are working with Python and tools related to Python.

## Exercise 1
Write a Python script that receives a number from std-in and verifies if the number is odd or even. If the number is even this script prints out "The number is even", otherwise it prints out "The number is odd"

You can access scripts arguments through a standard library module called `sys`. In that module you can access a variable (of type list) called `argv`. For example, you can print out all the arguments passed into a script as follows.

	import sys
	
	for x in sys.argv:
		print x

Note that the first value in sys.argv is the script name. You can use list slicing and the internal function `len()` verify the correctness of the scripts' input.

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
Enough math of problems! Let's start doing something more realistic. Let's write a little Phonebook framework to store contact informations and let's use the OOP features of Python to do so.

Let's start by creating some [DTO](http://en.wikipedia.org/wiki/Data_transfer_object) which represents a Contact entry.

	class Contact(object):
	    def __init__(self, name, address, phone_number):
	        self.name = name
	        self.address = address
	        self.phone_number = phone_number
	
	    def __str__(self):
	        return '{0} {1} {2}'.format(self.name, self.address, self.phone_number)

Place this class into file called contact.py

Open up a interactive Python shell in the same folder as the contact.py file resides in and create instance of the Contact class to verify that it works.

	Python 2.7.5 (default, Mar  9 2014, 22:15:05)
	[GCC 4.2.1 Compatible Apple LLVM 5.0 (clang-500.0.68)] on darwin
	Type "help", "copyright", "credits" or "license" for more information.
	>>> from contact import Contact
	>>> c1 = Contact('hlysig', 'Huldudalur 9', '8931337')
	>>> print c1
	hlysig Huldudalur 9 8931337
	>>>
	
What does the `__init__` function do and what does the `__str__` function do? Go to the Python manual and read about these functions.

Now we have a simple class that can store contact informations.
	
# Exercise 4
Let's write another class that represents a Phonebook. This class should allow us to add contacts, search for contacts and remove contacts. To begin with, let's store the contact instances in memory.

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
			Adds contact to the Phone book.
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
			Remove contact from the Phonebook
			"""
			pass
		
		def count(self):
			"""
			Returns the number of contacts in the
			Phonebook
			"""
			pass

Implement the missing functions in the class. Place this class after the Contact class in contact.py

# Exercise 5
After we have implemented the Phonebook we have no idea if it works correctly or not. We could write a Python code that uses our Phonebook and verify by hand if it works or not, but let's be more pro than that. Let's write some unit tests for our implementation.

Python has a built in [Unit test framework](https://docs.python.org/2/library/unittest.html) that is easy to use. Skim through it and then let's get busy writing some unit tests.

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

Running the test using the Python unit test framework is rather painful, but there is a fantastic project which extends the Python test framework that makes this task less easier. This project is called [nose](https://nose.readthedocs.org/en/latest/).

Before we use nose, we must install it. If you've gone through the Python environment installation guide that is on Github you have most likely installed tools called virtualenv and Pip. If not, got to that tutorial and read about these tools.

Let's create a virtualenv and install nose so we can run the test. In the folder where test_phonebook.py and  contacts.py reside, do the following.

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
Let's create a simple Flask web service that exposes our Phonebook. Let's do this together on the monitor.
		