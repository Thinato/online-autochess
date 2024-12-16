import Phaser from 'phaser'

export class Textbox extends Phaser.GameObjects.Container {
    name: string

    constructor(
        scene: Phaser.Scene,
        _name: string,
        x: number,
        y: number,
        _placeholder: string,
    ) {
        super(scene, x, y)
        this.name = _name

        scene.load.html(this.name, './view.html')

        const element = scene.add.dom(400, 0).createFromCache(this.name)

        scene.add.existing(this)
    }
}
