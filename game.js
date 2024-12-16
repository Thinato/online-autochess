
import './style.css'
import Phaser, { Physics } from 'phaser'

const gameSize = {
    width: 500,
    height: 500,
}

const speedDown = 300

class GameScene extends Phaser.Scene {
    constructor() {
        super('game-scene')
        this.player
        this.cursor
        this.playerSpeed = speedDown + 50
        this.target
        this.points = 0
    }

    preload() {
        this.load.image('bg', 'assets/bg.png')
        this.load.image('basket', 'assets/basket.png')
        this.load.image('apple', 'assets/apple.png')
        this.load.image('money', 'assets/money.png')
    }

    create() {
        this.add.image(0, 0, "bg").setOrigin(0, 0)

        this.player = this.physics.add.image(0, gameSize.height - 100, "basket").setOrigin(0, 0)
        this.player.setImmovable(true)
        this.player.body.allowGravity = false
        this.player.setCollideWorldBounds(true)

        this.target = this.physics.add.image(0, 0, "apple").setOrigin(0, 0)

        this.physics.add.collider(this.player, this.target, this.targetHit, null, this)

        this.cursor = this.input.keyboard.createCursorKeys()

        this.textScore = this.add.text(10, 10, `Score: ${this.points}`, { fontSize: '20px', fill: '#000' })

    }

    update() {
        if (this.target.y > gameSize.height) {
            this.target.x = Math.random() * gameSize.width
            this.target.y = 0
            this.target.setVelocityY(0)
        }

        const { left, right } = this.cursor


        if (left.isDown) {
            this.player.setVelocityX(-this.playerSpeed)
        } else if (right.isDown) {
            this.player.setVelocityX(this.playerSpeed)
        } else {
            this.player.setVelocityX(0)
        }
    }

    targetHit() {
        this.target.x = Math.random() * gameSize.width - 50
        this.target.y = 0
        this.target.setVelocityY(0)
        this.points++
        this.textScore.setText(`Score: ${this.points}`)
    }
}

const config = {
    type: Phaser.WEBGL,
    canvas: gameCanvas,
    physics: {
        default: 'arcade',
        arcade: {
            gravity: { y: speedDown },
            debug: true
        }
    },
    scene:
        [GameScene]
    ,
    ...gameSize,
}

const game = new Phaser.Game(config)