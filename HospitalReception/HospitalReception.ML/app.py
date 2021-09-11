
from flask import Flask
import tensorflow as tf
import pandas as pd
from sklearn.preprocessing import StandardScaler
from flask_cors import CORS
import pickle

global scaler
# Create an instance of the Flask class that is the WSGI application.
# The first argument is the name of the application module or package,
# typically __name__ when using a single module.
app = Flask(__name__)
CORS(app)

# Flask route decorators map / and /hello to the hello function.
# To add other resources, create functions that generate the page contents
# and add decorators to define the appropriate resource locators for them.



@app.route('/')
@app.route('/predictAtherosclerosisRisk')
def predictAs():
    input = scaler.transform([[ 55, 1, 1, 0, 0, 0, 1, 0, 0, 150,  26.66667,  14.7,
          5.76, 241, 118, 123, 255, 123, 132,  93, 231, 203, 0, 1]])
    # Render the page
    model = tf.keras.models.load_model("C://Users/mdemchenko/ML/Atherosclerosis/sens_06")

    output = model.predict(input)
    #output = model.predict([[ 0.22330696,  2.8013314 ,  1.07980327, -0.38701387, -0.15981325,
    #    -0.18898224,  0.23354968, -0.31455901, -0.25555063, -1.81460599,
    #    -0.51448037,  5.83454619,  0.33369126,  4.17863728,  2.01706804,
    #     4.57025888,  4.75782042,  2.1746554 ,  5.60847843,  1.84350387,
    #     3.14557646,  1.96364353, -0.53155929,  2.82842712]])
    return str(round(output[0][0],2))


@app.route('/predictInfarctionRisk')
def predictInfarction():
    input = [[53, 1, 0, 1, 0, 0, 0, 0, 0, 2, 5, 6, 1, 0, 1]]
    # Render the page
    loaded_model = pickle.load(open("C://Users/mdemchenko/ML/Atherosclerosis/pima_pickle.dat", "rb"))
    y_pred = loaded_model.predict(input)
    y_pred2 = loaded_model.predict_proba(input)[:, 1]
    #output = model.predict([[ 0.22330696,  2.8013314 ,  1.07980327, -0.38701387, -0.15981325,
    #    -0.18898224,  0.23354968, -0.31455901, -0.25555063, -1.81460599,
    #    -0.51448037,  5.83454619,  0.33369126,  4.17863728,  2.01706804,
    #     4.57025888,  4.75782042,  2.1746554 ,  5.60847843,  1.84350387,
    #     3.14557646,  1.96364353, -0.53155929,  2.82842712]])
    return str(y_pred2)

if __name__ == '__main__':
    # Run the app server on localhost:4449
    ds_std = pd.read_csv('dataset.csv')
    scaler = StandardScaler().fit(ds_std)
    app.run('localhost', 4449)