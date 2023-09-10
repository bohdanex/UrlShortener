import React, { Component } from 'react';
import URLTable from './url/URLTable';
import { TableUrl } from '../types/UrlTypes';
import { getAll } from './url/urlActinos';
import { getUserInfo } from './auth/authActions'
import Notifications from '../services/notificationService'
import {JWT_STORAGE_KEY} from '../constants'
import storage from 'react-secure-storage'
import { RootState } from '../store';
import { connect } from 'react-redux';

const notifications = new Notifications();

type State = {
  urls: Array<TableUrl>
}
type Props = {
  user: User
}

export class Home extends Component<Props, State> {

  constructor(props){
    super(props)

    this.state= {
      urls: []
    }
  }
  async componentDidMount(){
    const urls = await getAll();
    this.setState({urls});
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

const mapStateToProps = (state: RootState) => ({
  user: state.user.user
});

export default connect(mapStateToProps)(Home)