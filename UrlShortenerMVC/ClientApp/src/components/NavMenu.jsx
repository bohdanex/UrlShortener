import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import { Dispatch } from 'redux';
import {JWT_STORAGE_KEY} from '../constants'
import { RootState } from '../store'
import {addUser,remove} from '../store/features/userSlice'
import { connect } from 'react-redux';
import { Role, User } from '../types/Users';
import { getUserInfo } from './auth/authActions'
import storage from 'react-secure-storage'

type Props = {
  user: User
}

class NavMenu extends Component<Props> {
  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  async componentDidMount(){
    const accessToken = storage.getItem(JWT_STORAGE_KEY);
    if(accessToken != null){
      try{
        const user = await getUserInfo(accessToken);
        this.props.addUser(user)
      }
      catch(error){
      }
    }
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">Url Shortener</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>
                {this.props.user == null 
                ? 
                  <>
                      <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/login">Login</NavLink>
                      </NavItem>
                      <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/register">Register</NavLink>
                      </NavItem>
                  </>
                :<NavItem>
                  <NavLink className="text-dark btn"
                    
                  onClick={() => {
                    storage.removeItem(JWT_STORAGE_KEY);
                    this.props.remove();
                  }}>Logout</NavLink>
                </NavItem>}
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}
const mapStateToProps = (state: RootState) => ({
  user: state.user.user
});
const mapDispatchToProps = {
  addUser,
  remove
};

export default connect(mapStateToProps, mapDispatchToProps)(NavMenu)