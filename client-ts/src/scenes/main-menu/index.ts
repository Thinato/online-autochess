import { Button } from '../../components/button'
import { Textbox } from '../../components/textbox'
import constants from '../constants'
import Phaser from 'phaser'

export class MainMenu extends Phaser.Scene {
    usernameTextbox: Textbox
    passwordTextbox: Textbox
    loginButton: Button
    cursor: Phaser.GameObjects.Image

    constructor() {
        super({ key: constants.scenes.mainMenu.index })
    }

    preload() {}

    create() {
        this.usernameTextbox = new Textbox(this, 'txtUsername', 100, 100, 'Username')
        this.passwordTextbox = new Textbox(this, 'txtPassword', 100, 200, 'Password')
        this.loginButton = new Button(this, 'btnLogin', 100, 300, 'Login', () => {
            console.log('login')
        })
        this.cursor = this.add.image(0, 0, 'cursor').setOrigin(0, 0)
    }

    update() {
        const pointer = this.input.activePointer
        this.cursor.x = pointer.x
        this.cursor.y = pointer.y
        this.loginButton.update()
    }
}
