import sys


def show_help():
    """
    Function for printing out help on how to use this script.
    """
    print 'usage: ./oddeven [number1, number2, .. number_n]'
    print 'Iterates over number and tells if they are even or odd'


def is_even(value):
    """
    Checks if a given value is even.
    If value is even the function returns True, otherwise it returns False.
    """
    return value % 2 == 0

# If the script is executed directly from command line, then __name__ becomes
# __main__. Thus, this code is only executed if the script is executed
# from the command line. This "guard" is used to prevent code from being
# executed if the package is imported from another script.
if __name__ == '__main__':
    if len(sys.argv) == 1:
        print 'Missing argument to evaluate'
        print
        show_help()
        sys.exit(1)

    # We loop through all the values in sys.argv. This list contains
    # all the values that are passed in as arguments to the script.
    # The first parameter is the name of the script, therefore we
    # slice the list from by skipping the first element of the list.
    for n in sys.argv[1:]:
        # The values in sys.argv are of type string, thus we need to typecast
        # each element to an integer before we can use them in calculations.
        n = int(n)
        if is_even(n):
            print '{0} is even'.format(n)
        else:
            print '{0} is odd'.format(n)
