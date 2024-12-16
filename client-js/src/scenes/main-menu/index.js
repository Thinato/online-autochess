import { Button } from "../../components/button";
import constants from "../constants";
import Phaser from 'phaser'

export class MainMenu extends Phaser.Scene {
    cursor
    loginButton

    constructor() {
        super({ key: constants.scenes.mainMenu.index });
    }

    preload() {

    }

    create() {
        this.cursor = this.add.image(0, 0, 'cursor').setOrigin(0, 0)
        this.loginButton = new Button(this, 100, 100, 'Login', () => {
            console.log('login')
        })
    }

    update() {
        const pointer = this.input.activePointer
        this.cursor.x = pointer.x
        this.cursor.y = pointer.y

        this.loginButton.update()

    }
}