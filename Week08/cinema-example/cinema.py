import json
import sys

import requests
from termcolor import colored


def show_running_movies():
    """
    Fetches movies that are beeing shown in Icelandic theaters and
    prints them out to stdout
    """
    # Do a get request to cinema api at api.is
    response = requests.get('http://apis.is/cinema')

    # If we don't receive a 2xx status code, we stop and print error.
    if not response.ok:
        print 'Unable to read from api, got non ok status code: {0}'\
            .format(response.status_code)
        sys.exit(1)

    # The respond is on json format. We use python json module to
    # parse the json
    # and map it into a Python associated array (dictionary)
    d = json.loads(response.text)

    # We loop the result and print it to stdout.
    for res in d.get('results'):
        print colored(res.get('title').encode('utf-8'), 'yellow')
        for showtime in res.get('showtimes'):
            print ' ', showtime.get('theater').encode('utf-8')
            for schedule in showtime.get('schedule'):
                print '  ', schedule.encode('utf-8')

if __name__ == '__main__':
    show_running_movies()
