import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { Link } from 'react-router-dom';
import { AuthenticationService } from '../../Services/AuthenticationService';

export default function Header(){
    return(
    <Navbar expand="lg" className="bg-body-tertiary">
      <Container>
        <Navbar.Brand>
          <Link to={"/home"} className='nav-link'>Library</Link>
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link>
              <Link to={"/home"} className='nav-link'>Home</Link>
            </Nav.Link>
            <Nav.Link>
              <Link to={"/books"} className='nav-link'>Books</Link>
            </Nav.Link>
            {AuthenticationService.userRole === "admin" &&(
              <Nav.Link>
                <Link to={"/admin"} className='nav-link'>Admin</Link>
              </Nav.Link>
            )}
            <NavDropdown title="Profile" className='nav-link'>
              {!AuthenticationService.userName && (
                <NavDropdown.Item href='/login'>Login</NavDropdown.Item>
              )}
              {AuthenticationService.userName && (
                <>
                  <NavDropdown.Item href='/profile'>{AuthenticationService.userName}</NavDropdown.Item>
                  <NavDropdown.Divider />
                  <NavDropdown.Item href='/logout'>Logout</NavDropdown.Item>
                </>
              )}
            </NavDropdown>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
    )
}