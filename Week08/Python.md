#Python programming language 

Python is easy to learn, powerful programming language.  Python has efficient high-level data structures, 
and a simple but effective approach to object-oriented programming.    Python has an elegant syntax and dynamic typing.    
Python can be downloaded from: [https://www.python.org/]

Python syntax is defined in a style guide 	that can be found here, 
[http://legacy.python.org/dev/peps/pep-0008/#code-lay-out].
The key rule for python is to use 4 spaces pr. indentation level, do not use tabs.  
There are no curly brackets around the body of a Python function.  It starts after the colon in the next new line.   
Classes and functions should be separated by a blank line. 

Python is interpreted language, which means that the interpreter takes the code and compiles it and runs it at the same time. Code is interpreted to byte code which is then executed.   There is no precompile needed as in C# and java. 

Python is a dynamic language and that’s way you don’t need to specify the type of a variable.  When 'a = 10' is declared Python interpreter knows that this variable is an integer.  It is also possible to change the data type of a variable, 'a' can fec. be changed into string later in a function. The built in function: type(x), tells us of what type a variable or an object is. The function: dir(x) gives us all functions and properties the object x has. 

It is possible to declare and assignee values to man variables at the same time. 
```
a, b, c = 10, 20, 30
```

Strings are powerful in Python, it is possible to work with strings as an array. 
```
> a = 'Hlynur er hetja'
> a[0:4]
> 'Hlyn'
> a[-1]
> 'a'
```
Python offers many string functions that can be found in the documentation.

A function in Python is defined with the keyword def: 
```
> def add(a, b):
>     return a+b
>
> x = add(10, 20)  -- 30
```
Functions in Python will always return value, if there is no return statement the function will return 'none' 
which is the null value in Python.   Function in Python can also return many values (variables).   

Function can have optional parameters, 
```
> def add(a, b, variation=10):
>     return a+b
>
> x = add(10, 20)   -- 30
> y = add(10, 20, variation=2)   -- 32
```

Functions can also have key-value pair parameters. 
```
> def add(**kwargs):
>     return kwargs.get('a') + kwargs.get('b')
>
> x = add(a=10, b=20)   -- 30
```

Here is how you define a list in Python:
```
> x = [1, 'x', 3.5, 41]
```
It is possible to have different types of values in an array but that should be avoided.   
Python offers built in string functions, see the documentation. 

Dictionaries are defined like this:
```
> x = {'kalli':20, 'hlynur':10}
> x.get('kalli')  -- get key
> x.['kalli'] = 20  -- set key
```

Control flow is similar to other programming languages. 
```
if True:
    pass
Elif False:
    Pass
Else:
    pass
```

Python has True and False, but in Python you can set almost anything as Boolean value, it is called truthfulness 
value of an object. 
```
> x = None
> If x:
    print x
> Else:
    print 'None'
```
    
This means that if the variable x has some value it will be printed out, otherwise the control flow goes to the else 
part and 'None' will be printed out. 

For loops are defined like this:
```
> x = [1, 2, 3]
> for a in x:     
    print a;
```

Python has multiple inheritance.  Classes are defined like this:
```
> classs Gun(object): 
    def __init__(self, gun_type, amo):    -- default constructor, has to take in instance of it self 
    self.gun_type = gun_type
    self.amo = amo    
>
> var x = Gun(‘Beretta’, 12)
> print x.gun_type
```

Python has no public or private declarations of properties in classes.   

