import React, {Component} from "react";
import { Table } from "reactstrap";
import { TableUrl } from "../../types/UrlTypes";
import { User, Role } from "../../types/Users";
import moment from 'moment'
import {RxTrash} from 'react-icons/rx'

type Props = {
    urls: Array<TableUrl>,
    profile:? User,
    deleteUrl: (urlId: string) => void
}

export default class URLTable extends Component<Props>{
    renderDeleteButton(url: TableUrl){
        const profile = this.props.profile;
        if(profile == null || profile.role === null || (profile.role === Role.User && profile.id !== url.userId)){
            return;
        }
        return (<>
            <RxTrash style={{cursor: "pointer"}} color="orange" onClick={() => this.props.deleteUrl(url.id)}/>
        </>);
    }
    renderUrl = (url: TableUrl, index: number) => {
        return (<tr key={index}>
            <td><a href={url.originalURL}>{url.shortenedURL}</a></td>
            <td><a href={url.originalURL}>{url.originalURL}</a></td>
            <td>{moment(url.creationDate).format('lll')}</td>
            <td>{this.renderDeleteButton(url)}</td>
        </tr>);
    }
    
    render(){
        return (<Table striped bordered responsive size="sm">
            <thead>
                <tr>
                    <td>Shortened</td>
                    <td>Original</td>
                    <td>Creation date</td>
                    <td></td>
                </tr>
            </thead>
            <tbody>
                {this.props.urls.map(this.renderUrl)}
            </tbody>
        </Table>)
    }
}