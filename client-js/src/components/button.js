import Phaser from 'phaser';


export class Button extends Phaser.GameObjects.Container {
    constructor(scene, x, y, text, onClick) {
        super(scene, x, y);

        this.text = scene.add.text(0, 0, text, { fill: '#ffffff', fontSize: 24 });
        this.text.setOrigin(0.5, 0.5);
        this.add(this.text);

        this.setInteractive(new Phaser.Geom.Rectangle(0, 0, this.text.width, this.text.height), Phaser.Geom.Rectangle.Contains);
        this.on('pointerdown', onClick);

        scene.add.existing(this);
    }
}