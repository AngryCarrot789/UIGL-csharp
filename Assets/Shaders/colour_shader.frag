#version 150
precision highp float;

uniform vec4 in_colour;

void main(void) {
    gl_FragColor = in_colour;
}