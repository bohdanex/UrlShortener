import React, { RefObject } from "react";
import {Form, FormFeedback, FormGroup, Input, Button, Label, Row, Col, NavLink, NavItem} from 'reactstrap'
import { login } from './authActions'
import { UserAuth, AuthResponse } from "../../types/Users";
import { ErrorResponse } from "../../types/Responses";
import { createRef } from "react";
import Notifications from "../../services/notificationService";
const notifications = new Notifications(); 

type Props = {

}

type State = {
    isLoggingIn: boolean,
    userAuth: UserAuth,
    isValidEmail: Boolean,
    isValidPassword: Boolean
}

export default class LoginForm extends React.Component<Props, State>{
    constructor(props){
        super(props);

        this.state = {
            isLoggingIn: false,
            userAuth: new UserAuth(),
        }

        this.emailInputRef = createRef();
        this.passwordInputRef = createRef();
    }

    emailInputRef: RefObject<HTMLInputElement>;
    passwordInputRef: RefObject<HTMLInputElement>;
    async login(){
        const user = new UserAuth();
        user.email = this.emailInputRef.current.value;
        user.password = this.passwordInputRef.current.value;
        
        const resultObj = await login(user);
        if(resultObj.errorMessage != null){
            notifications.error(resultObj.errorMessage)
        }
        else{
            notifications.success("Succesfully logged in")
            location.assign('/');
        }
    }

    render(){
        return (<Form method="post" className="position-relative">
         <FormGroup>
            <Label htmlFor="email">Email</Label>
            <Input type="email" 
                innerRef={this.emailInputRef}
                id="email"
                placeholder="example@mail.com"/>
         </FormGroup>
         <FormGroup>
            <Label htmlFor="password">Password</Label>
            <Input type="password"
                id="password"
                innerRef={this.passwordInputRef}/>
         </FormGroup>
         <FormGroup>
             <Row>
                <Col> 
                    <Button type="button" color="success"
                    onClick={this.login.bind(this)}>Login</Button>
                </Col>
             </Row>
         </FormGroup>
        </Form>);
    }
}