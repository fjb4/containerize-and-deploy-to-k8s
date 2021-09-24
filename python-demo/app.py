import os, datetime
from flask import Flask
app = Flask(__name__)

@app.route("/")
def hello_world():
    pod_name = os.environ.get('POD_NAME')
    return "<p>Hello, World from Python at {0}!</p><p>Pod Name: {1}</p>".format(datetime.datetime.now(), pod_name)