#include "tree.h"




Tree::Tree(Tool::Struct::Vector3 * vector3) : Plant(vector3) {
    this->type = RoleType::Tree;
}

Tree::~Tree() {
}
