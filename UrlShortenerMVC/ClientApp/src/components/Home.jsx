import React, { Component } from 'react';
import URLTable from './url/URLTable';
import { TableUrl } from '../types/UrlTypes';
import { getAll } from './url/urlActinos';

type State = {
  urls: Array<TableUrl>
}
export class Home extends Component {

  constructor(props){
    super(props)

    this.state= {
      urls: []
    }
  }
  componentDidMount(){
    getAll().then(urls=>this.setState({urls}))
  }
  
  render () {
    return (
      <div>
        <h2>URL info table</h2>
        <URLTable urls={this.state.urls} />
      </div>
    );
  }
}
