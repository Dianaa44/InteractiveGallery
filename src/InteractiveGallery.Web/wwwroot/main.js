import * as THREE from 'three';
import { OrbitControls } from 'three/addons/controls/OrbitControls.js';
import { GLTFLoader } from 'three/addons/loaders/GLTFLoader.js';



//===================================================== define scen , loader , renderer
var renderer = new THREE.WebGLRenderer({ alpha: true, antialiase: true });
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);
const loader = new GLTFLoader();
var camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
var scene = new THREE.Scene();



//===================================================== Controls
const controls = new OrbitControls(camera, renderer.domElement);
controls.enableKeys = false;
controls.enableDamping = true; 
controls.dampingFactor = 0.1;
//===================================================== scene
var scene = new THREE.Scene();
const raycaster = new THREE.Raycaster();
//===================================================== camera
camera.position.set(0, 1, -7.5);
//camera.lookAt(0, 1,7.5 );

var moveSpeed = 0.05;
var rotateSpeed = 0.02;

document.addEventListener('keydown', function (event) {
    switch (event.key) {
        case 'ArrowUp':
            camera.translateZ(-moveSpeed);
            break;
        case 'ArrowDown':
            camera.translateZ(moveSpeed);
            break;
        case 'ArrowLeft':
            camera.rotation.y += rotateSpeed;
            break;
        case 'ArrowRight':
            camera.rotation.y -= rotateSpeed;
            break;
    }
});



var imagesNum = 0;
function addImage(image) {
    const imageGeometry = new THREE.PlaneGeometry(1, 1);
    //// Load image texture
    const textureLoader = new THREE.TextureLoader();
    const imageTexture = textureLoader.load(image);
    const imageMaterial = new THREE.MeshBasicMaterial({ map: imageTexture });
    const imagePlane = new THREE.Mesh(imageGeometry, imageMaterial);
    imagesNum += 1;
    var p = new THREE.Vector3();
    p=findImagePosition();
    console.log(p);
    imagePlane.position.set(p.x,p.y,p.z);
    console.log(imagePlane.position);
    if (imagesNum<=12 && imagesNum % 3 == 1) imagePlane.rotateY(Math.PI);
    else if ((imagesNum <= 12 && imagesNum % 3 == 2) || (imagesNum > 12 && imagesNum % 2 == 1)) imagePlane.rotateY(-Math.PI / 2);
    else imagePlane.rotateY(Math.PI / 2);
    console.log(imagePlane.position)
    scene.add(imagePlane);
}

for (var i = 0; i<16; i++) {
    addImage(`${image1}`)
}
function findImagePosition() {
    if (imagesNum <= 12) {
        if (imagesNum % 3 == 1) {
            var ix = (Math.floor(imagesNum / 3) + 1) * 2-5;
            return new THREE.Vector3(ix,2,7.45);
        }
        else if (imagesNum % 3 == 2) {
            var iz = (Math.floor(imagesNum / 3) + 1) * 2 - 7;
            return new THREE.Vector3(4.95, 2, iz);
        }
        else {
            var iz = (Math.floor(imagesNum / 3) + 1) * 2 - 10;
            return new THREE.Vector3(-4.95, 2, iz);
        }
    }
    else if (imagesNum <= 16) {
        if (imagesNum % 2 == 1) {
            var iz = (Math.floor((imagesNum-12) / 2) + 5) * 2 - 7;
            return new THREE.Vector3(4.95, 2, iz);
        }
        else {
            var iz = (Math.floor((imagesNum - 12) / 2) + 5) * 2 - 10;
            return new THREE.Vector3(-4.95, 2, iz);
        }
    }
}

//===================================================== load model
var mixer;
var model;

loader.load(
    `${assetUrl}`, function (gltf) {
        console.log('ffffff')
        gltf.scene.traverse(function (node) {
            if (node instanceof THREE.Mesh) {
                node.castShadow = true;
                node.material.side = THREE.DoubleSide;
            }
        });
        model = gltf.scene;
      //  model.scale.set(.35, .35, .35);
        model.position.set(0, 1, 0)
        scene.add(model);
        
        mixer = new THREE.AnimationMixer(model);
        mixer.clipAction(gltf.animations[1]).play();

});

   
//========================================render
var clock = new THREE.Clock();
function render() {
    requestAnimationFrame(render);
    var delta = clock.getDelta();
    if (mixer != null) mixer.update(delta);
    //if (model) model.rotation.y += 0.025;

    renderer.render(scene, camera);
}
render();




//=====================================================
function animate() {
    requestAnimationFrame(animate);
    controls.update();
    renderer.render(scene, camera);
}
animate();