#version 150

// Globals
uniform mat4 projection;

// Inputs
in vec3 in_pos;

void main(void) {
    // vec4 pos = projection * view * vec4(in_pos, 1.0);
    // gl_Position = vec4(pos.x - 1.0f, pos.y + 1.0f, pos.z, pos.w);
    // gl_Position = projection * view * vec4(in_pos, 1.0);
    gl_Position = projection * vec4(in_pos, 1.0);
}