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
          <Link to={"/home"} className='nav-link'>React-Bootstrap</Link>
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
            <Nav.Link>
              <Link to={"/profile"} className='nav-link'>Profile</Link>
            </Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
    )
}