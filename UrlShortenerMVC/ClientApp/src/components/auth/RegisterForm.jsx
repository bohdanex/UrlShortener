import React, { RefObject, isValidElement } from "react";
import {Form, FormFeedback, FormGroup, Input, Button, Label, Row, Col, ButtonGroup} from 'reactstrap'
import { register } from './authActions'
import { UserAuth } from "../../types/Users";
import { createRef } from "react";
import { withRouter } from 'react-router-dom';
type Props = {
    
}

type State = {
    isLoggingIn: boolean,
    userAuth: UserAuth,
    isValidEmail: Boolean,
    isValidPassword: Boolean,
    isPasswordMatch: Boolean
}

class LoginForm extends React.Component<Props, State>{
    constructor(props){
        super(props);

        this.state = {
            isLoggingIn: false,
            userAuth: new UserAuth(),
            isValidEmail: undefined,
            isValidPassword: undefined,
            isPasswordMatch: undefined
        }

        this.emailInputRef = createRef();
        this.passwordInputRef = createRef();
        this.rePasswordInputRef = createRef();
    }

    emailInputRef: RefObject<HTMLInputElement>;
    passwordInputRef: RefObject<HTMLInputElement>;
    rePasswordInputRef: RefObject<HTMLInputElement>;
    async register(){
        const user = new UserAuth();
        user.email = this.emailInputRef.current.value;
        user.password = this.passwordInputRef.current.value;
        this.validateInputs(async () => { 
            if(await register(user)){ 
                location.assign('/');
            }
        });
    }
    validateInputs(callback:? Function){
        const state = this.state;
        state.isValidEmail = !this.emailInputRef.current.value == '';

        const password = this.passwordInputRef.current.value;
        state.isValidPassword = true;
        if(password.length < 8 || password.includes(' ')){
            state.isValidPassword = false;
        }
        
        if(password.length > 0 || this.rePasswordInputRef.current.value.length > 0){
            const isPasswordMatch = this.passwordInputRef.current.value == this.rePasswordInputRef.current.value;
            state.isPasswordMatch = isPasswordMatch;
        }

        this.setState(state, () => {
            if(state.isPasswordMatch == true && state.isValidPassword == true && state.isValidEmail == true){
                callback();
            }
        })
    }
    render(){
        return (<Form method="post" className="position-relative">
         <FormGroup>
            <Label htmlFor="email">Email</Label>
            <Input type="email" 
                innerRef={this.emailInputRef}
                id="email"
                placeholder="example@mail.com"
                valid={this.state.isValidEmail}
                invalid={this.state.isValidEmail === false}/>
            {this.state.isValidEmail === true && <FormFeedback>Email provided</FormFeedback>}
            {this.state.isValidEmail === false && <FormFeedback >Provide a valid email!</FormFeedback>}
         </FormGroup>
         <FormGroup>
            <Label htmlFor="password">Password</Label>
            <Input type="password"
                id="password"
                innerRef={this.passwordInputRef}
                valid={this.state.isValidPassword}
                invalid={this.state.isValidPassword === false}/>
            {this.state.isValidPassword === false && <FormFeedback>Enter a valid password</FormFeedback>}
         </FormGroup>
         <FormGroup>
            <Label htmlFor="rePassword">Confirm password</Label>
            <Input type="password" 
                id="rePassword"
                innerRef={this.rePasswordInputRef}
                valid={this.state.isPasswordMatch}
                invalid={this.state.isPasswordMatch === false}/>
                {this.state.isPasswordMatch === false && <FormFeedback>Passwords do not match!</FormFeedback>}
         </FormGroup>
         <FormGroup>
            <Button type="button" color="success"
                onClick={this.register.bind(this)}>Register</Button>
         </FormGroup>
         <FormGroup>
            <Button color="link" onClick={() => this.props.history.push('/login')}>
            Already have an account?
            </Button>
         </FormGroup>
        </Form>);
    }
}

export default withRouter(LoginForm);