import React, {Component} from "react";
import { BaseUrl } from "../../types/UrlTypes";
import {ListGroup, ListGroupItem} from 'reactstrap'
import { getById } from "./urlActinos";
import Notifications from "../../services/notificationService";
import moment from "moment";

const notifications = new Notifications();

type Props = {
    match: {
        params: {
            id: Number
        }
    }
}

type State = {
    baseUrl: BaseUrl
}

export default class UrlView extends Component<Props, State>{
    constructor(props){
        super(props);
        
        this.state = {
            baseUrl: new BaseUrl()
        }
    }
    async componentDidMount(){
        const baseUrl = await getById(this.props.match.params.id)
        if(baseUrl.errorMessage != null){
            notifications.error(baseUrl.errorMessage);
        }
        else{
            this.setState({baseUrl});
        }
    }
    render(){
        return (<>
            {this.state.baseUrl.id != null && 
            <>
                <h2>Created by: {this.state.baseUrl.user.email}</h2>
                <ListGroup>
                    <ListGroupItem>Original URL: {this.state.baseUrl.originalURL}</ListGroupItem>
                    <ListGroupItem>Shortened URL: {this.state.baseUrl.shortenedURL}</ListGroupItem>
                    <ListGroupItem>Creation date: {moment(this.state.baseUrl.creationDate).format('LLL')}</ListGroupItem>
                </ListGroup>
            </>}
        </>);
    }
}