// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

contract ChasingLoveGame {
    mapping(address => bool) private openGate;
    mapping(address => uint256) private interactionCount;
    mapping(address => uint256) private scoreSaving;

    // Events
    event openGateEvent(address indexed player);
    event interactionCountEvent(address indexed player, uint256 newCount);
    event scoreSavingEvent(address indexed player, uint256 newSaving);

    function hasOpenGate(address player) external view returns (bool) {
        return openGate[player];
    }

    function claimOpenGate(address player) external {
        openGate[player] = true;
        emit openGateEvent(player);
    }

    function getInteractionCount(
        address player
    ) external view returns (uint256) {
        return interactionCount[player];
    }

    function giveInteractionCount(address player) external {
        interactionCount[player] += 1;
        emit interactionCountEvent(player, interactionCount[player]);
    }

    function getScoreSaving(address player) external view returns (uint256) {
        return scoreSaving[player];
    }

    function giveScoreSaving(address player, uint256 score) external {
        scoreSaving[player] = score;
        emit scoreSavingEvent(player, scoreSaving[player]);
    }
}
