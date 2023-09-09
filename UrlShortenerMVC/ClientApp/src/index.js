import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { store } from './store';
import {Provider} from 'react-redux'
import {ReactNotifications} from 'react-notifications-component'

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

ReactDOM.render(
  <BrowserRouter basename={baseUrl}>
    <ReactNotifications />
    <App />
  </BrowserRouter>,
  rootElement);

registerServiceWorker();

