import Phaser from 'phaser'

export class Button extends Phaser.GameObjects.Container {
    text: Phaser.GameObjects.Text
    background: Phaser.GameObjects.Rectangle
    name: string

    constructor(
        scene: Phaser.Scene,
        _name: string,
        x: number,
        y: number,
        _text: string,
        onClick: () => void,
    ) {
        super(scene, x, y)
        this.name = _name

        scene.load.html(this.name, './view.html')


        this.background = scene.add
            .rectangle(0, 0, 200, 50, 0x0a0a0a)
            .setOrigin(0, 0)

        this.add(this.background)

        this.text = scene.add.text(0, 0, _text, {
            fontSize: 24,
            color: '#ffffff',
        })
        this.add(this.text)

        this.setInteractive(
            new Phaser.Geom.Rectangle(
                this.background.x,
                this.background.y,
                this.background.width,
                this.background.height,
            ),
            Phaser.Geom.Rectangle.Contains,
        )
        this.on('pointerdown', onClick)

        scene.add.existing(this)
    }
}
