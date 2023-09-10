import React, { Component } from 'react';
import URLTable from './url/URLTable';
import { TableUrl } from '../types/UrlTypes';
import { getAll, deleteUrl, addUrl } from './url/urlActinos';
import { getUserInfo } from './auth/authActions'
import Notifications from '../services/notificationService'
import { RootState } from '../store';
import { connect } from 'react-redux';
import { User } from '../types/Users';
import {Button, Row, Col} from 'reactstrap'
const notifications = new Notifications();

type State = {
  urls: Array<TableUrl>,
  page: number,
  newUrl: String,
  newlyAddedUrls: Array<TableUrl>
}
type Props = {
  user: User
}

class Home extends Component<Props, State> {

  constructor(props){
    super(props)

    this.state= {
      urls: [],
      page: 0,
      newUrl: '',
      newlyAddedUrls: []
    }
  }
  async componentDidMount(){
    const urls = await getAll(this.state.page);
    this.setState({urls});
  }
  
  deleteUrl = async (ulrId: string, deleteNewlyCreated = false) => {
    const result = await deleteUrl(ulrId);
    if(result.errorMessage != null){
      notifications.error(result.errorMessage);
      return;
    }
    const urls: Array<TableUrl> = deleteNewlyCreated === false ? this.state.urls : this.state.newlyAddedUrls;
    const urlToDelete = urls.find(x => x.id === ulrId);
    const deleteIndex = urls.indexOf(urlToDelete);
    urls.splice(deleteIndex, 1);
    if(deleteNewlyCreated === false){
      this.setState({urls});
    }
    else{
      this.setState({newlyAddedUrls: urls});
    }
  }

  deleteNewlyAddedUrl = async (ulrId: string) => {
    await this.deleteUrl(ulrId, true);
  }

  addNewUrl = async () => {
    try{
      const url = await addUrl(this.state.newUrl);
      const newlyAddedUrls = this.state.newlyAddedUrls;
      newlyAddedUrls.unshift(url);
      this.setState({newlyAddedUrls})
    }
    catch(ex){
      notifications.error(ex.message);
    }

  }

  renderAddNewUrl = () => {
    return (<Row>
      <Col style={{display: "grid"}} xs={3}>
        <Button color='success' onClick={this.addNewUrl}>Add new URL</Button>
      </Col>
      <Col xs={9}>
        <textarea placeholder='https://someurl...' className='form-control'
        onChange={(evt) => this.setState({newUrl: evt.target.value})}
        value={this.state.newUrl}></textarea>
      </Col>
    </Row>)
  }

  render () {
    return (
      <div>
        <h2>URL info table</h2>
        <URLTable 
        urls={this.state.urls} 
        profile={this.props.user}
        deleteUrl={this.deleteUrl}/>
        
        {this.state.newlyAddedUrls.length > 0 
        && [<h3 key={0}>Newly added URLs</h3>,
         <URLTable key={1}
        urls={this.state.newlyAddedUrls} 
        profile={this.props.user}
        deleteUrl={this.deleteNewlyAddedUrl}/>]}
        {this.props.user != null && this.renderAddNewUrl() }
      </div>
    );
  }
}

const mapStateToProps = (state: RootState) => ({
  user: state.user.user
});
export default connect(mapStateToProps)(Home);