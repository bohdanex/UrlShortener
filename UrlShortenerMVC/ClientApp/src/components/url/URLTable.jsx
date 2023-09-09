import React, {Component} from "react";
import { Table } from "reactstrap";
import { TableUrl } from "../../types/UrlTypes";
import { User } from "../../types/Users";


type Props = {
    urls: Array<TableUrl>,
    profile: User
}

export default class URLTable extends Component<Props>{
    renderUrl(url: TableUrl, index: number){
        return (<a href="#">
            <tr>
                <td>{url.shortenedURL}</td>
                <td>{url.fullURL}</td>
                <td>{url.creationDate}</td>
            </tr>
        </a>);
    }
    
    render(){
        <Table>
            <thead>
                <tr>
                    <td>Shortened Url</td>
                    <td>Original URls</td>
                    <td>Creation date</td>
                </tr>
            </thead>
            <tbody>
                {this.props.urls.map(this.renderUrl)}
            </tbody>
        </Table>
    }
}