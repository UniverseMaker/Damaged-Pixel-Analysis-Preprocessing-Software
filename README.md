# Damaged-Pixel-Analysis-Preprocessing-Software
Damaged Pixel Analysis Pre-processing Software for DPA-Server based on C# Language. This software requires analysis server. 손상 픽셀 분석 전처리 소프트웨어이며 본 시스템을 사용하기 위해서는 분석 서버가 추가적으로 필요합니다.

# 공개대기중
# Introduction
![image](https://github.com/UniverseMaker/Damaged-Pixel-Analysis-Preprocessing-Software/assets/14816515/4e754102-f59d-4fc2-b7fa-c40380005ccf)
RGB 이미지와 다분광 이미지를 바탕으로 분석할 범위(사각형)을 지정하고 불필요한 부분을 마스킹할 수 있는 기능이 있습니다.
승인 번호를 바탕으로 분석서버에 전송하며 범위내의 손상 픽셀을 면적으로부터 역산하는 기능을 제공합니다.
Based on RGB images and multispectral images, there is a function to specify a range (rectangle) to be analyzed and mask unnecessary parts.
Based on the approval number, it is transmitted to the analysis server and provides a function of inverting the damaged pixels within the range from the area.

![image](https://github.com/UniverseMaker/Damaged-Pixel-Analysis-Preprocessing-Software/assets/14816515/fff6332b-67cd-47c7-a6d8-13bda66de55c)
소프트웨어 초기화면
Software initial screen

![image](https://github.com/UniverseMaker/Damaged-Pixel-Analysis-Preprocessing-Software/assets/14816515/1f42b702-804a-4d3b-bd50-4e2d3f52666b)
파일 메뉴를 통해 이미지를 불러올 수 있습니다.
Images can be loaded through the File menu.

![image](https://github.com/UniverseMaker/Damaged-Pixel-Analysis-Preprocessing-Software/assets/14816515/dc820897-d6f7-4943-8d02-97d9be3d1202)
캘리브레이션: 정상픽셀과 손상픽셀을 지정해 표본을 산출할 수 있습니다.
범위선택: 분석범위(사각형) 설정 > 첫클릭 좌측상단, 두번째클릭 우측하단
범위제한: 분석범위 내 불필요한 부분 (예, 조명으로 인한 색상이상등) 마스킹
전송준비: 이미지 레스터화
서버전송: 승인번호를 바탕으로 서버로 이미지 업로드 및 손상픽셀 면적산출 요청
Calibration: Samples can be calculated by specifying normal pixels and damaged pixels.
Range selection: analysis range (rectangular) setting > first click upper left corner, second click lower right corner
Scope limitation: Masking of unnecessary parts within the analysis range (e.g., color abnormality due to lighting)
Preparing for Transfer: Rasterizing the Image
Server transmission: Based on the approval number, upload the image to the server and request damage pixel area calculation
