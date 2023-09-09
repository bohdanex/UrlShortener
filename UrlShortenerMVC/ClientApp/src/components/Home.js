import React, { Component } from 'react';
import URLTable from './url/URLTable';

export class Home extends Component {
  static displayName = Home.name;
  componentDidMount(){

  }
  render () {
    return (
      <div>
        <URLTable />
      </div>
    );
  }
}
