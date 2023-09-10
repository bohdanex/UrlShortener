import React, {Component} from "react";
import { Table } from "reactstrap";
import { TableUrl } from "../../types/UrlTypes";
import { User } from "../../types/Users";
import moment from 'moment'

type Props = {
    urls: Array<TableUrl>,
    profile: User
}

export default class URLTable extends Component<Props>{
    renderUrl(url: TableUrl, index: number){
        return (<tr key={index}>
            <td><a href={url.originalURL}>{url.shortenedURL}</a></td>
            <td><a href={url.originalURL}>{url.originalURL}</a></td>
            <td>{moment(url.creationDate).format('lll')}</td>
        </tr>);
    }
    
    render(){
        return (<Table striped bordered responsive size="sm">
            <thead>
                <tr>
                    <td>Shortened</td>
                    <td>Original</td>
                    <td>Creation date</td>
                </tr>
            </thead>
            <tbody>
                {this.props.urls.map(this.renderUrl)}
            </tbody>
        </Table>)
    }
}