import {Button, Container, Menu} from "semantic-ui-react";
import React from "react";

interface Props {
    openForm: () => void;
}

export default function NavBar({openForm}: Props) {
    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item header>
                    <a href="http://localhost:3000/">
                    <img src='/assets/logo.png' alt='logo' style={{marginRight: 10, width: 150}}/>
                    </a>
                </Menu.Item>
                <Menu.Item name='Activities' />
                <Menu.Item>
                    <Button onClick={openForm} positive content='Create Activity' />
                </Menu.Item>
            </Container>
        </Menu>
    )
}