#!flask/bin/python
from flask import Flask, jsonify
from flask import make_response
from flask import request
from radon.cli.harvest import CCHarvester,HCHarvester,RawHarvester,MIHarvester
from radon.cli import Config
from radon.complexity import SCORE

c = Config(
    functions= None,
    by_function= None,
    multi=None,
    exclude="C:\TestFields\PythonTest/venv, C:\TestFields\PythonTest/.vscode",
    ignore='venv,.vscode',
    no_assert= None,
    order=SCORE,
    show_closures=False,
    min='A',
    max='F',
)
 
app = Flask(__name__)
 
@app.route('/api/metrics/cc', methods=['POST'])
def get_ccMetrics():
    pathList= (list)(request.json['pathList'])
    ccHarvester = CCHarvester(pathList, c)
    resultCC = ccHarvester.as_json()
    pathList.clear()
    return resultCC

@app.route('/api/metrics/hc', methods=['POST'])
def get_hcMetrics():
    pathList= (list)(request.json['pathList'])
    hcHarvester=HCHarvester(pathList,c)
    resultHC = hcHarvester.as_json()
    pathList.clear()
    return resultHC

@app.route('/api/metrics/mi', methods=['POST'])
def get_miMetrics():
    pathList= (list)(request.json['pathList'])
    miHarvester= MIHarvester(pathList,c)
    resultMI= miHarvester.as_json()
    pathList.clear()
    return resultMI

@app.route('/api/metrics/raw', methods=['POST'])
def get_rawMetrics():
    pathList= (list)(request.json)
    rawHarvester= RawHarvester(pathList,c)
    resultRAW = rawHarvester.as_json()
    pathList.clear()
    return resultRAW


@app.errorhandler(404)
def not_found(error):
    return make_response(jsonify({'HTTP 404 Error': 'The content you looks for does not exist. Please check your request.'}), 404)


if __name__ == '__main__':
    app.run(debug=True)#!flask/bin/python