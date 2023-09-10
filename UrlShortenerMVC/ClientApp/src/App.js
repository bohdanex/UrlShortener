import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import Home  from './components/Home';
import RegisterForm from './components/auth/RegisterForm';
import LoginForm from './components/auth/LoginForm';
import './custom.css'


export default class App extends Component {
  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/register' component={RegisterForm} />
        <Route path='/login' component={LoginForm} />
      </Layout>
    );
  }
}
