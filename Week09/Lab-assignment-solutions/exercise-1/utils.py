import memcache
memcached_host = '127.0.0.1:11211'


def get_score(direction):
    client = memcache.Client([memcached_host], debug=True)
    value = client.get(direction)

    if not value:
        client.set(direction, 0)
        value = 0

    return value


def incr_score(direction):
    client = memcache.Client([memcached_host], debug=True)
    value = client.get(direction)

    if not value:
        client.set(direction, 1)
    else:
        client.incr(direction)
