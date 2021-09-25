import os, datetime
from flask import Flask
app = Flask(__name__)

@app.route("/")
def hello_world():
    pod_name = os.environ.get('POD_NAME')
    return "Hello, World from Python at {0}!\nPod Name: {1}".format(datetime.datetime.now(), pod_name)