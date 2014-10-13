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
