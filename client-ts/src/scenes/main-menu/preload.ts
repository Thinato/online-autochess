import Phaser from 'phaser'
import constants from '../constants'

export class MainMenuPreload extends Phaser.Scene {
    constructor() {
        super({ key: constants.scenes.mainMenu.preload })
    }

    preload() {
        this.load.image('cursor', 'assets/img/cursors/dwarven_gauntlet.png')
    }

    create() {
        this.scene.start(constants.scenes.mainMenu.index)
    }
}
