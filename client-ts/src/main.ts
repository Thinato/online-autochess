import { MainMenu } from './scenes/main-menu'
import { MainMenuPreload } from './scenes/main-menu/preload'
import Phaser from 'phaser'

var canvas: HTMLCanvasElement = document.getElementById(
    'gameCanvas',
) as HTMLCanvasElement

const gameSize = {
    width: 500,
    height: 500,
}

const config = {
    type: Phaser.WEBGL,
    canvas,
    scene: [MainMenuPreload, MainMenu],
    ...gameSize,
}

const game = new Phaser.Game(config)
