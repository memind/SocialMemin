import { Link } from 'react-router-dom';
import { Button, Container, Header, Segment, Image } from "semantic-ui-react";

export default function HomePage() {
    return (
        <Segment inverted textAlign='center' vertical className='masthead' >
            <Container text>
                    <Image size='massive' src='/assets/logo.png' alt='logo' style={{ marginBottom: 12 }} />
                
                <Header as='h2' inverted content='Welcome to Social Memin' />
                <Button as={Link} to='/activities' size='huge' inverted>
                    Take Memin to the activities!
                </Button>
            </Container>
        </Segment>
    )
}