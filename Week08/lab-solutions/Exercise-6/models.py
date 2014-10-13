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
